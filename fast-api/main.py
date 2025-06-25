from fastapi import FastAPI
import os
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

app = FastAPI()

@app.get("/")
def read_root():
    # Access the environment variable "ENVX"
    envx_value = os.getenv("ENVX", "Not Set")

    return {"message": "Under Renovation type shi v7.", "envx": envx_value}
