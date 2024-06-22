import { ConfirmationService } from 'primeng/api';
import { ConfirmPopup } from 'primeng/confirmpopup';
import { catchError, EMPTY, finalize, Observable, takeUntil, tap } from 'rxjs';

import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { TeacherGatewayView } from '../../../../core/models/teacher';
import { FacultyService } from '../../../../core/services/faculty.service';
import { ToastrService } from '../../../../core/services/toastr.service';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { CreateTeacherRequest } from '../../../models/users-management.models';
import { UsersManagementService } from '../../../services/users-management.service';
import { selectManagedFacultyId } from '../../../state/workspace.selectors';
import { chnuEmailValidator } from '../../validators/fields.validators';

@Component({
    selector: 'uni-teachers',
    templateUrl: './teachers.component.html',
    styleUrls: ['./teachers.component.scss'],
})
export class TeachersComponent extends BaseComponent implements OnInit {
    @ViewChild(ConfirmPopup) confirmPopup: ConfirmPopup;

    teacherForm: FormGroup;

    facultyId = null;
    teachers$: Observable<TeacherGatewayView[]>;

    isLoading: boolean = true;
    isCreateTeacherSidebarVisible = false;

    selectedTeachers: TeacherGatewayView[] = [];

    constructor(
        private userManagementService: UsersManagementService,
        private store: Store,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private translate: TranslateService,
        private confirmationService: ConfirmationService,
        private facultyService: FacultyService
    ) {
        super();
    }

    ngOnInit(): void {
        this.teacherForm = this.fb.group({
            email: [
                '',
                [Validators.required, Validators.email, chnuEmailValidator()],
            ],
            fullName: ['', Validators.required],
            position: ['', Validators.required],
        });

        this.store
            .select(selectManagedFacultyId)
            .pipe(takeUntil(this.destroy$))
            .subscribe(facultyId => {
                if (facultyId) {
                    this.facultyId = facultyId;
                    this.teachers$ = this.fetchTeachers(facultyId);
                }
            });
    }

    accept() {
        this.confirmPopup.accept();
    }

    reject() {
        this.confirmPopup.reject();
    }

    openAddTeacherSidebar() {
        this.isCreateTeacherSidebarVisible = true;
    }

    confirmDeletionTeachers(event: Event) {
        this.confirmationService.confirm({
            target: event.target as EventTarget,
            message: this.translate.instant(
                this.selectedTeachers.length === 1
                    ? 'teachers_control.delete_confirmation_single'
                    : 'teachers_control.delete_confirmation',
                { count: this.selectedTeachers.length }
            ),
            accept: () => {
                this.userManagementService
                    .deleteUsers(
                        this.facultyId,
                        this.selectedTeachers.map(u => u?.userId)
                    )
                    .pipe(
                        tap(() => {
                            this.teachers$ = this.fetchTeachers(this.facultyId);
                        }),
                        catchError(() => {
                            this.toastr.error(
                                this.translate.instant('common.error'),
                                this.translate.instant('add_teacher.error')
                            );
                            return EMPTY;
                        })
                    )
                    .subscribe();
            },
            reject: () => {
                this.selectedTeachers = [];
            },
        });
    }

    onSubmit() {
        if (this.teacherForm.invalid) {
            return;
        }

        const payload: CreateTeacherRequest = {
            ...this.teacherForm.value,
            facultyIds: [this.facultyId],
        };

        this.userManagementService.createTeacher(payload).subscribe({
            next: () => {
                this.toastr.success(
                    this.translate.instant('common.done'),
                    this.translate.instant('add_teacher.success')
                );
                this.isCreateTeacherSidebarVisible = false;
                this.teachers$ = this.fetchTeachers(this.facultyId);
                this.teacherForm.reset();
            },
            error: () => {
                this.toastr.error(
                    this.translate.instant('common.error'),
                    this.translate.instant('add_teacher.error')
                );
            },
        });
    }

    getTeachersFacultiesNames(teacher: TeacherGatewayView): string {
        return teacher.faculties.map((f) => f.name).join(', ');
    }

    private fetchTeachers(facultyId) {
        return this.userManagementService.getTeachers(facultyId).pipe(
            catchError(() => {
                this.isLoading = false;
                this.toastr.error(
                    this.translate.instant('common.error'),
                    this.translate.instant('errors.something_went_wrong')
                );
                return EMPTY;
            }),
            finalize(() => {
                this.isLoading = false;
            })
        );
    }
}
