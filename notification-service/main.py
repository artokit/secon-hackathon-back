import uvicorn
from fastapi import FastAPI
from routers.email_notification import router as email_notification_router

app = FastAPI(docs_url="/notification-service/docs", openapi_url="/notification-service/openapi.json")
app.include_router(email_notification_router, prefix="/notification-service")


if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=8001)
