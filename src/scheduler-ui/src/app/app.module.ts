import { CookieService } from 'ngx-cookie-service';

import { LayoutModule } from '@angular/cdk/layout';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
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
import { AuthInterceptor } from './core/interceptors/http.interceptor';
import { Language } from './core/models/languages';
import { appEffects } from './core/state/effects';
import { metaReducers, reducers } from './core/state/reducers';
import { DashboardModule } from './dashboard/dashboard.module';
import { ScheduleModule } from './schedule/schedule.module';
import { SharedModule } from './shared/shared.module';

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
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        SharedModule,
        AuthModule,
        DashboardModule,
        ScheduleModule,
        HttpClientModule,
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
    ],
    providers: [
        TranslateStore,
        {
            provide: APP_INITIALIZER,
            useFactory: LanguageLoader,
            multi: true,
            deps: [TranslateService, CookieService],
        },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
