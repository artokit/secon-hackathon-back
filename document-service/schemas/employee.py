import datetime
from dataclasses import dataclass


@dataclass
class EmployeeVacation:
    department: str
    position: str
    name: str
    vacation_days: int
    advanced_days: int
    planned_date: datetime.datetime


@dataclass
class EmployeeVacationSendData:
    employees: list[EmployeeVacation]