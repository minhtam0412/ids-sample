import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AuthService} from '../services/auth.service';

@Component({
  selector: 'app-call-api',
  templateUrl: './call-api.component.html',
  styles: []
})
export class CallApiComponent implements OnInit {
  response: any[];

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  ngOnInit() {
    const x = this.authService.getAuthorizationHeaderValue();
    console.log(x);
    const headers = new HttpHeaders({Authorization: this.authService.getAuthorizationHeaderValue()});

    this.http.get('https://localhost:5001/api/Test', {headers})
      .subscribe(response => {
        this.response = response as any[];
        console.log(response);
      });
  }

}
