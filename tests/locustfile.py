import time
from locust import HttpUser, task, between

class LoadTestEmployeeService(HttpUser):
    wait_time = between(1, 2.5)    
    path = "/Employee.svc/api/v1/GetHiresByYear"

    @task
    def get_hires_by_year(self):
        self.client.get(self.path)

    def on_start(self):
        self.client.get(self.path)