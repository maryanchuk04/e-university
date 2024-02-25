'use client';

import { NextUIProvider } from '@nextui-org/react';
import { ThemeProvider as NextThemesProvider, ThemeProvider } from 'next-themes';

const Providers = ({ children }: { children: React.ReactNode }) => {
    return (
        <NextUIProvider>
            <ThemeProvider attribute='class' defaultTheme='dark'>
                {children}
            </ThemeProvider>
        </NextUIProvider>
    );
};

export default Providers;
