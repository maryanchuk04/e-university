import { Component } from '@angular/core';

import { mockUser } from '../../../core/models/user-view';
import { MediaService } from '../../../core/services/media.service';

@Component({
    selector: 'uni-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrl: './nav-menu.component.scss',
})
export class NavMenuComponent {
    user = mockUser;
    constructor(private media: MediaService) { }

    showUserInfo = false;

    toggleUserInfo() {
        this.showUserInfo = !this.showUserInfo;
    }
}
