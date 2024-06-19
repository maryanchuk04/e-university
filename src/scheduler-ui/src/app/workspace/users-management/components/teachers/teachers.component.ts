import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';

import { Teacher } from '../../../../core/models/teacher';
import { TeacherService } from '../../../../core/services/teacher.service';
import { BaseComponent } from '../../../../shared/components/base/base.component';

@Component({
  selector: 'uni-teachers',
  templateUrl: './teachers.component.html',
  styleUrl: './teachers.component.scss'
})
export class TeachersComponent extends BaseComponent implements OnInit{
    private teachers$: Observable<Teacher[]>;

    constructor(private teacherService: TeacherService) { super(); }

    ngOnInit(): void {
        // this.teacherService.get()
    }
}
