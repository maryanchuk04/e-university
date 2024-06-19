import { MenuItem } from 'primeng/api';

import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({
    selector: 'uni-workspace-nav-menu',
    templateUrl: './workspace-nav-menu.component.html',
    styleUrl: './workspace-nav-menu.component.scss',
})
export class WorkspaceNavMenuComponent extends BaseComponent {
    items: MenuItem[] | undefined;

    constructor(private translate: TranslateService) {
        super();
    }

    ngOnInit() {
        this.items = [
            {
                separator: true,
            },
            {
                label: 'workspace.menu.users_management.sub_title',
                items: [
                    {
                        label: 'workspace.menu.users_management.students',
                        icon: 'pi pi-users',
                        url: '/workspace/users-management/students'
                    },
                    {
                        label: 'workspace.menu.users_management.teachers',
                        icon: 'pi pi-users',
                        url: '/workspace/users-management/teachers'
                    },
                ],
            },
            {
                label: 'workspace.menu.educational_process.sub_title',
                items: [
                    {
                        label: 'workspace.menu.educational_process.subjects',
                        icon: 'pi pi-book',
                    },
                    {
                        label: 'workspace.menu.educational_process.schedule',
                        icon: 'pi pi-calendar',
                    },
                    {
                        label: 'workspace.menu.educational_process.сall_schedule',
                        icon: 'pi pi-calendar-clock',
                    },
                    {
                        label: 'Logout',
                        icon: 'pi pi-sign-out',
                    },
                ],
            },

        ];
    }
}
