import { catchError, EMPTY, map, takeUntil } from 'rxjs';

import { GoogleLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { Role } from '../../core/models/role';
import { UserProvider } from '../../core/providers/user.provider';
import { MediaService, ScreenSize } from '../../core/services/media.service';
import { ToastrService } from '../../core/services/toastr.service';
import { BaseComponent } from '../../shared/components/base/base.component';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'uni-authenticate',
    templateUrl: './authenticate.component.html',
})
export class AuthenticateComponent extends BaseComponent implements OnInit {
    ScreenSize = ScreenSize;

    constructor(
        private socialAuthService: SocialAuthService,
        private authService: AuthService,
        private media: MediaService,
        private router: Router,
        private userProvider: UserProvider,
        private toastr: ToastrService,
        private translate: TranslateService
    ) {
        super();
    }

    ngOnInit(): void {
        this.socialAuthService.authState
            .pipe(takeUntil(this.destroy$))
            .subscribe(user => {
                this.authService
                    .authenticate({
                        email: user.email,
                        picture: user.photoUrl,
                        fullName: user.name,
                    })
                    .pipe(
                        takeUntil(this.destroy$),
                        map(() => {
                            const user = this.userProvider.getCurrentUser();
                            if (!user) {
                                this.toastr.error(
                                    this.translate.instant('common.error'),
                                    this.translate.instant(
                                        'authenticate.errors.something_went_wrong'
                                    )
                                );
                                return;
                            }

                            if (this.userProvider.hasAdminAccess()) {
                            }

                            if (this.userProvider.hasAccessToDashboard())
                                this.router.navigate(['/']);

                            if (user.role === Role.Teacher)
                                this.router.navigate(['/teacher']);
                        }),
                        catchError((err) => {
                            if (err.status && err.status === 400) {
                                this.toastr.error("Помилка", "Ви повинні використовувати корпоративний обліковий запис @chnu.edu.ua")
                            }

                            return EMPTY;
                        })
                    )
                    .subscribe();
            });
    }

    login() {
        this.socialAuthService
            .signIn(GoogleLoginProvider.PROVIDER_ID)
            .then(() => EMPTY);
    }

    getScreenSize = () => this.media.getScreenSize();
}
