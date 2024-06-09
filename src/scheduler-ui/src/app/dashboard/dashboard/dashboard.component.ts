import { Component, OnInit } from '@angular/core';

import { Role } from '../../core/models/role';
import { UserProvider } from '../../core/providers/user.provider';
import { StudentService } from '../../core/services/student.service';

@Component({
    selector: 'uni-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
    constructor(private userProvider: UserProvider, private studentService: StudentService) {}

    ngOnInit(): void {
        const user = this.userProvider.getCurrentUser();
        if (user.role === Role.Student) {
            this.studentService.getCurrentStudent().subscribe(x => console.log(x));
        }
    }
}
