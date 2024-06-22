import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { StudentGatewayView } from '../../core/models/student-gateway-view';
import { TeacherGatewayView } from '../../core/models/teacher';
import { BaseHttpService } from '../../core/services/base.service';
import { CreateStudentRequest, CreateTeacherRequest } from '../models/users-management.models';

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

    getTeachers(facultyId: string): Observable<TeacherGatewayView[]> {
        return this.http.get(`${this.baseUrl}/${facultyId}/teachers`);
    }

    deleteUsers(facultyId: string, userIds: string[]): Observable<void>{
        return this.http.post(`${this.baseUrl}/${facultyId}/delete/users`, { userIds: userIds });
    }

    createTeacher(payload: CreateTeacherRequest): Observable<string>{
        return this.http.post<string>(`api/teacher`, payload);
    }
}
