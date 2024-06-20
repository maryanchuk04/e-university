import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { AuthService } from '../../../auth/services/auth.service';
import { StudentGatewayView } from '../../../core/models/student-gateway-view';
import { selectStudent } from '../../../core/state/student/student.selectors';

interface MenuItem {
    icon: string;
    link: string;
    label: string;
}

const navigationLinks: MenuItem[] = [
    {
        icon: 'pi-home',
        link: '/dashboard',
        label: 'nav_menu.dashboard',
    },
    {
        icon: 'pi-calendar-clock',
        link: '/dashboard/schedule',
        label: 'nav_menu.schedule',
    },
    {
        icon: 'pi-clock',
        link: '/dashboard/timetable',
        label: 'nav_menu.timetable',
    },
    {
        icon: 'pi-user',
        link: '/dashboard/profile/student',
        label: 'nav_menu.profile',
    },
];

@Component({
    selector: 'uni-sidebar-menu',
    templateUrl: './sidebar-menu.component.html',
    styleUrls: ['./sidebar-menu.component.scss'],
})
export class SidebarMenuComponent implements OnInit {
    student$: Observable<StudentGatewayView>;

    constructor(private store: Store, private auth: AuthService) {}

    ngOnInit(): void {
        this.student$ = this.store.select(selectStudent);
    }

    menu = navigationLinks;
    isCollapsed = false;

    toggleSidebar() {
        this.isCollapsed = !this.isCollapsed;
    }

    logout() {
        this.auth.logout();
        window.location.href = "/authenticate"
    }
}
