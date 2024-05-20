import { Component, OnInit } from '@angular/core';

import { EMPTY, takeUntil } from 'rxjs';

import { GoogleLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';

import { BaseComponent } from '../../shared/components/base.component';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'uni-authenticate',
    templateUrl: './authenticate.component.html',
})
export class AuthenticateComponent extends BaseComponent implements OnInit {
    constructor(private socialAuthService: SocialAuthService, private authService: AuthService) {
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
}
