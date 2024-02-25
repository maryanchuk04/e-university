import createMiddleware from 'next-intl/middleware';
import { Language, supportedLanguages } from './app/components/LanguageSwitcher/Language';

export default createMiddleware({
    locales: supportedLanguages,

    defaultLocale: Language.ua,
});

export const config = {
    matcher: ['/', '/(ua|en)/:path*'],
};
