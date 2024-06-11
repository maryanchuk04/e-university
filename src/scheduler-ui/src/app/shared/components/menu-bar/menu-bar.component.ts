import { Component } from '@angular/core';

interface MenuItem {
    icon: string;
    link: string;
}

const navigationLinks: MenuItem[] = [
    {
        icon: 'pi-home',
        link: '/dashboard'
    },
    {
        icon: 'pi-calendar-clock',
        link: '/dashboard/schedule',
    },
    {
        icon: 'pi-clock',
        link: '/dashboard/timetable',
    },
    {
        icon: 'pi-user',
        link: '/dashboard/profile/student',
    }
]

@Component({
    selector: 'uni-menu-bar',
    templateUrl: './menu-bar.component.html',
    styleUrl: './menu-bar.component.scss'
})
export class MenuBarComponent {
    menu = navigationLinks;
}
