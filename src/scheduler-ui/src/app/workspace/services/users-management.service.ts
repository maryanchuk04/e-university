import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { StudentGatewayView } from '../../core/models/student-gateway-view';
import { BaseHttpService } from '../../core/services/base.service';
import { CreateStudentRequest } from '../models/users-management.models';

@Injectable({
    providedIn: 'root',
})
export class UsersManagementService {
    private baseUrl = 'api/users-management/faculty'

    constructor(private http: BaseHttpService) {}

    createStudent(payload: CreateStudentRequest): Observable<string> {
        return this.http.post<string>(`${this.baseUrl}/${payload.facultyId}/student`, payload);
    }

    getStudents(facultyId: string): Observable<StudentGatewayView[]> {
        return this.http.get(`${this.baseUrl}/${facultyId}/students`);
    }

    getTeachers(facultyId: string): Observable<StudentGatewayView[]> {
        return this.http.get(`${this.baseUrl}/${facultyId}/teachers`);
    }

    deleteUsers(facultyId: string, userIds: string[]): Observable<void>{
        return this.http.post(`${this.baseUrl}/${facultyId}/delete/users`, { userIds: userIds });
    }
}
