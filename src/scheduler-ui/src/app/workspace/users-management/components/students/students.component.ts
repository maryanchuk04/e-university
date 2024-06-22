import { ConfirmationService } from 'primeng/api';
import { ConfirmPopup } from 'primeng/confirmpopup';
import { catchError, EMPTY, finalize, Observable, takeUntil, tap } from 'rxjs';

import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { GroupInfoGatewayView } from '../../../../core/models/group-gateway-view';
import { StudentGatewayView } from '../../../../core/models/student-gateway-view';
import { GroupService } from '../../../../core/services/group.service';
import { ToastrService } from '../../../../core/services/toastr.service';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { CreateStudentRequest } from '../../../models/users-management.models';
import { UsersManagementService } from '../../../services/users-management.service';
import { selectManagedFacultyId } from '../../../state/workspace.selectors';
import { chnuEmailValidator } from '../../validators/fields.validators';

interface PermissionOption {
    label: string;
    value: number;
}

@Component({
    selector: 'uni-students',
    templateUrl: './students.component.html',
    styleUrl: './students.component.scss',
})
export class StudentsComponent extends BaseComponent implements OnInit {
    @ViewChild(ConfirmPopup) confirmPopup: ConfirmPopup;

    studentForm: FormGroup;

    facultyId = null;
    students$: Observable<StudentGatewayView[]>;
    groups: GroupInfoGatewayView[];

    isLoading: boolean = true;
    isCreateStudentSidebarVisible = false;

    selectedStudents: StudentGatewayView[] = [];

    permissionsOptions: PermissionOption[];

    constructor(
        private userManagementService: UsersManagementService,
        private store: Store,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private translate: TranslateService,
        private confirmationService: ConfirmationService,
        private groupService: GroupService
    ) {
        super();

        this.permissionsOptions = [
            {
                label: this.translate.instant('permission.full_access'),
                value: 8,
            },
            {
                label: this.translate.instant('permission.faculty_admin'),
                value: 4,
            },
            {
                label: this.translate.instant('permission.schedule_viewer'),
                value: 1,
            },
        ];
    }

    ngOnInit(): void {
        this.studentForm = this.fb.group({
            email: [
                '',
                [Validators.required, Validators.email, chnuEmailValidator()],
            ],
            groupId: ['', Validators.required],
            permissions: [[], Validators.required],
        });

        this.store
            .select(selectManagedFacultyId)
            .pipe(takeUntil(this.destroy$))
            .subscribe(facultyId => {
                if (facultyId) {
                    this.facultyId = facultyId;
                    this.students$ = this.fetchStudents(facultyId);
                    this.groupService
                        .getByFaculty(facultyId)
                        .pipe(takeUntil(this.destroy$))
                        .subscribe(groups => {
                            this.groups = groups;
                        });
                }
            });
    }

    accept() {
        this.confirmPopup.accept();
    }

    reject() {
        this.confirmPopup.reject();
    }

    openAddStudentSidebar() {
        this.isCreateStudentSidebarVisible = true;
    }

    confirmDeletionStudents(event: Event) {
        this.confirmationService.confirm({
            target: event.target as EventTarget,
            message: this.translate.instant(
                this.selectedStudents.length === 1
                    ? 'students_control.delete_confirmation_single'
                    : 'students_control.delete_confirmation',
                { count: this.selectedStudents.length }
            ),
            accept: () => {
                this.userManagementService
                    .deleteUsers(this.facultyId, this.selectedStudents.map((u) => u?.userId))
                    .pipe(
                        tap(() => {
                            this.students$ = this.fetchStudents(this.facultyId);
                        }),
                        catchError(() => {
                            this.toastr.error(
                                this.translate.instant('common.error'),
                                this.translate.instant('add_student.error')
                            );
                            return EMPTY;
                        })
                    )
                    .subscribe();
            },
            reject: () => {
                this.selectedStudents = [];
            },
        });
    }

    onSubmit() {
        if (this.studentForm.invalid) {
            return;
        }

        const payload: CreateStudentRequest = {
            ...this.studentForm.value,
            facultyId: this.facultyId,
        };

        this.userManagementService.createStudent(payload).subscribe({
            next: () => {
                this.toastr.success(
                    this.translate.instant('common.done'),
                    this.translate.instant('add_student.success')
                );
                this.isCreateStudentSidebarVisible = false;
                this.students$ = this.fetchStudents(this.facultyId);
                this.studentForm.reset();
            },
            error: () => {
                this.toastr.error(
                    this.translate.instant('common.error'),
                    this.translate.instant('add_student.error')
                );
            },
        });
    }

    private fetchStudents(facultyId) {
        return this.userManagementService.getStudents(facultyId).pipe(
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
