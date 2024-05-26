import { Component } from '@angular/core';

interface MenuItem {
    icon: string;
    link: string;
}

const navigationLinks: MenuItem[] = [
    {
        icon: 'pi-home',
        link: '/'
    },
    {
        icon: 'pi-calendar-clock',
        link: 'schedule',
    },
    {
        icon: 'pi-calendar-clock',
        link: 'schedule',
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
