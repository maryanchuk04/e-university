import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthModel } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
    url = `${environment.gatewayBaseAddress}/api/authenticate`

    constructor(private http: HttpClient) {
    }

    authenticate(request: AuthModel): Observable<any> {
        return this.http.post(this.url, request);
    }
}
