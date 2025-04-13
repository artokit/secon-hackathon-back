import os
import uuid
import requests
from fastapi import APIRouter
from schemas.employee import EmployeeVacationSendData
from services.form import fill_t7_form_with_openpyxl
from services.orders import create_and_upload_vacation_schedule_order

router = APIRouter(prefix="/orders")


@router.post("/order")
async def order_document(count_order: int):
    res = create_and_upload_vacation_schedule_order(count_order)
    return res["url"]


@router.post("/form")
async def form_document(employee_data: EmployeeVacationSendData):
    file_id = uuid.uuid4()
    file_name = f'{file_id}.xlsx'

    fill_t7_form_with_openpyxl(employee_data, output_file=file_name)
    with open(file_name, 'rb') as f:
        files = {
            'file': (file_name, f, 'application/vnd.openxmlformats-officedocument.wordprocessingml.document')
        }

        headers = {
            'accept': 'application/json'
        }

        response = requests.post(
            'https://hackathon-calendar.duckdns.org/file-service/file',
            headers=headers,
            files=files
        )

    os.remove(file_name)
    return response.json()["url"]
