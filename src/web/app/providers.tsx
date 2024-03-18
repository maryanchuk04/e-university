'use client';

import { NextUIProvider } from '@nextui-org/react';
import { SessionProvider } from 'next-auth/react';
import { ThemeProvider } from 'next-themes';

const Providers = ({ children }: { children: React.ReactNode, }) => {
    return (
        <SessionProvider>
            <NextUIProvider>
                <ThemeProvider attribute='class' defaultTheme='dark'>
                    {children}
                </ThemeProvider>
            </NextUIProvider>
        </SessionProvider>
    );
};

export default Providers;
