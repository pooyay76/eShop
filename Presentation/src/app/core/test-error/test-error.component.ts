import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;
  validationErrors: string[] = [];
  constructor(private httpClient: HttpClient) {

  }
  get404() {
    this.httpClient.get(this.baseUrl + "buggy/notfound").subscribe({
      next: response => console.log(response),
      error: response => console.log(response)
    }
    )
  }

  get500() {
    this.httpClient.get(this.baseUrl + "buggy/servererror").subscribe({
      next: response => console.log(response),
      error: response => console.log(response)
    })
  }
  get400() {
    this.httpClient.get(this.baseUrl + "buggy/badrequest").subscribe({
      next: response => console.log(response),
      error: response => console.log(response)
    })
  }
  getValidationError() {
    this.httpClient.get(this.baseUrl + "products/fortytwo").subscribe({
      next: response => console.log(response),
      error: response => {
        console.log(response);
        this.validationErrors = response.errors;
      }
    })
  }

}
