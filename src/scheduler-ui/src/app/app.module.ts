import { CookieService } from 'ngx-cookie-service';

import { LayoutModule } from '@angular/cdk/layout';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import {
    BrowserModule, HAMMER_GESTURE_CONFIG, HammerGestureConfig, HammerModule
} from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import {
    TranslateLoader, TranslateModule, TranslateService, TranslateStore
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { Language } from './core/models/languages';
import { appEffects } from './core/state/effects';
import { metaReducers, reducers } from './core/state/reducers';
import { DashboardModule } from './dashboard/dashboard.module';
import { ScheduleModule } from './schedule/schedule.module';
import { SharedModule } from './shared/shared.module';
import { WorkspaceModule } from './workspace/workspace.module';
import { LogoutComponent } from './logout/logout.component';

export class CustomHammerConfig extends HammerGestureConfig {
    override overrides = {
        swipe: { direction: Hammer.DIRECTION_HORIZONTAL },
    };
}

export function LanguageLoader(
    translate: TranslateService,
    cookieService: CookieService
): () => Promise<void> {
    let promise = new Promise<void>(resolve => {
        const cookieLang = cookieService.get('skLanguageCode');
        const browserLang = cookieLang || translate.getBrowserLang();
        const lang = browserLang?.match(/en|ua/) ? browserLang : Language.ua;

        translate.use(lang);
        resolve();
    });
    return async () => promise;
}

export function TranslationLoaderFactory(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/languages/', '.json');
}

@NgModule({
    declarations: [AppComponent, LogoutComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        SharedModule,
        AuthModule,
        DashboardModule,
        ScheduleModule,
        HttpClientModule,
        WorkspaceModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: TranslationLoaderFactory,
                deps: [HttpClient],
            },
            isolate: true,
        }),
        StoreModule.forRoot(reducers, { metaReducers }),
        EffectsModule.forRoot(appEffects),
        StoreDevtoolsModule.instrument(),
        LayoutModule,
        HammerModule,
    ],
    providers: [
        TranslateStore,
        {
            provide: APP_INITIALIZER,
            useFactory: LanguageLoader,
            multi: true,
            deps: [TranslateService, CookieService],
        },
        { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
