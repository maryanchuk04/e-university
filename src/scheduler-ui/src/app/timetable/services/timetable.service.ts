import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { UserProvider } from '../../core/providers/user.provider';

@Injectable({
    providedIn: 'root',
})
export class TimetableService {
    url = `${environment.gatewayBaseAddress}/api/timetable`;

    constructor(private http: HttpClient, private userProvider: UserProvider) {}

    getUserTimetable(): Observable<any> {
        const userId = this.userProvider.getCurrentUser().id;

        return this.http.post(`${this.url}/${userId}`, { withCredentials: true });
    }
}
