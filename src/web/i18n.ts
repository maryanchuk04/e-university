import { notFound } from 'next/navigation';
import { getRequestConfig } from 'next-intl/server';
import { supportedLanguages } from './app/components/LanguageSwitcher/Language';

const locales = supportedLanguages;

export default getRequestConfig(async ({ locale }) => {
	if (!locale) {
        return {
            messages: (await import(`./translations/ua.json`)).default,
        };
    }

    if (!locales.includes(locale as any)) notFound();

    return {
        messages: (await import(`./translations/${locale}.json`)).default,
    };
});
