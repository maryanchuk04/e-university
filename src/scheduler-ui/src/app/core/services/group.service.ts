import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { GroupInfoGatewayView } from '../models/group-gateway-view';
import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class GroupService {
    baseUrl = 'api/groups'

    constructor(private http: BaseHttpService) {}

    getByFaculty(facultyId: string): Observable<GroupInfoGatewayView[]>{
        return this.http.get(`${this.baseUrl}/faculty/${facultyId}`);
    }
}
