import createMiddleware from 'next-intl/middleware';
import { defaultLanguage, supportedLanguages } from './core/models/Language';
import { NextRequest } from 'next/server';

const I18nMiddleware = createMiddleware({
    locales: supportedLanguages,
    defaultLocale: defaultLanguage,
});

export function middleware(request: NextRequest) {
    return I18nMiddleware(request);
}

export const config = {
    matcher: ['/((?!api|static|.*\\..*|_next|favicon.ico|robots.txt).*)'],
};
