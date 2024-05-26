import { EMPTY, takeUntil, } from 'rxjs';

import { GoogleLoginProvider, SocialAuthService, } from '@abacritt/angularx-social-login';
import { BreakpointObserver, BreakpointState, } from '@angular/cdk/layout';
import { Component, OnInit, } from '@angular/core';

import { MediaService, ScreenSize, } from '../../core/services/media.service';
import { BaseComponent, } from '../../shared/components/base/base.component';
import { AuthService, } from '../services/auth.service';

@Component({
    selector: 'uni-authenticate',
    templateUrl: './authenticate.component.html',
})
export class AuthenticateComponent extends BaseComponent implements OnInit {
    ScreenSize = ScreenSize;

    constructor(
        private socialAuthService: SocialAuthService,
        private authService: AuthService,
        private media: MediaService
    ) {
        super();
    }

    ngOnInit(): void {
        this.socialAuthService.authState.pipe(takeUntil(this.destroy$)).subscribe((user) => {
            this.authService
                .authenticate({ email: user.email, picture: user.photoUrl, name: user.name })
                .pipe(takeUntil(this.destroy$))
                .subscribe((x) => console.log(x));
        });
    }

    login() {
        this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(() => EMPTY);
    }

    getScreenSize = () => this.media.getScreenSize();
}
