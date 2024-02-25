import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import { NextIntlClientProvider, useMessages } from 'next-intl';
import Providers from '../providers';
import '../globals.css';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
    title: 'e-University',
    description: 'e-University',
};

type Props = {
    children: React.ReactNode;
    params: {
        locale: 'ua' | 'en';
    };
};

const RootLayout = ({ children, params: { locale } }: Props) => {
    const messages = useMessages();
    return (
        <html lang={locale ?? 'ua'} suppressHydrationWarning={true}>
            <NextIntlClientProvider messages={messages}>
                <body className={inter.className} suppressHydrationWarning={true}>
                    <Providers>{children}</Providers>
                </body>
            </NextIntlClientProvider>
        </html>
    );
};

export default RootLayout;
