from fastapi import FastAPI
from routers.orders import router as order_router
import uvicorn


app = FastAPI(docs_url="/document-service/docs", openapi_url="/document-service/openapi.json")
app.include_router(order_router, prefix="/document-service")

if __name__ == "__main__":
    uvicorn.run(app, port=8002, host="0.0.0.0")
