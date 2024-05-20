import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GoogleLoginProvider, GoogleSigninButtonModule, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';

import { environment } from '../../environments/environment';
import { SharedModule } from '../shared/shared.module';
import { AuthenticateComponent } from './authenticate/authenticate.component';

const googleProvider = {
    provide: 'SocialAuthServiceConfig',
    useValue: {
        autoLogin: false,
        providers: [
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider(environment.google.clientId),
            },
        ],
        onError: (error) => {
            console.error(error);
        },
    } as SocialAuthServiceConfig,
};

@NgModule({
    declarations: [AuthenticateComponent],
    imports: [CommonModule, SharedModule, GoogleSigninButtonModule],
    providers: [googleProvider],
})
export class AuthModule {}
