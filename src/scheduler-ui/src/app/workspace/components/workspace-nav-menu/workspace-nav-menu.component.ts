import { MenuItem } from 'primeng/api';
import { Observable } from 'rxjs';

import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { ManagerGatewayView } from '../../../core/models/manager-gateway-view';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { selectManager } from '../../state/workspace.selectors';

@Component({
    selector: 'uni-workspace-nav-menu',
    templateUrl: './workspace-nav-menu.component.html',
    styleUrl: './workspace-nav-menu.component.scss',
})
export class WorkspaceNavMenuComponent extends BaseComponent {
    manager$: Observable<ManagerGatewayView>;

    isCollapsed = false;

    menu: MenuItem[] | undefined;

    constructor(private translate: TranslateService, private store: Store) {
        super();
    }

    ngOnInit() {
        this.manager$ = this.store.select(selectManager);

        this.menu = [
            {
                label: 'workspace.menu.users_management.sub_title',
                items: [
                    {
                        label: 'workspace.menu.users_management.students',
                        icon: 'pi pi-graduation-cap',
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
                ],
            },
        ];
    }

    toggleSidebar() {
        this.isCollapsed = !this.isCollapsed;
    }
}
