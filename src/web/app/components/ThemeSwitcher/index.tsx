'use client';

import React from 'react';
import { VisuallyHidden, useSwitch } from '@nextui-org/react';
import { Moon } from './Moon';
import { Sun } from './Sun';
import { useTheme } from 'next-themes';
import { Theme } from './Theme';

const ThemeSwitcher = (props: any) => {
    const { theme, setTheme } = useTheme();

    const changeTheme = () => {
        theme === Theme.Dark ? setTheme(Theme.Light) : setTheme(Theme.Dark);
    };

    const { Component, slots, getBaseProps, getInputProps, getWrapperProps } = useSwitch({ ...props, onChange: changeTheme });

    return (
        <div className='flex flex-col gap-2'>
            <Component {...getBaseProps()}>
                <VisuallyHidden>
                    <input {...getInputProps()} />
                </VisuallyHidden>
                <div
                    {...getWrapperProps()}
                    className={slots.wrapper({
                        class: [
                            'w-11 h-11',
                            'flex items-center justify-center',
                            'rounded-lg bg-default-100 hover:bg-default-200 dark:bg-primary dark:hover:bg-primary-300',
                        ],
                    })}
                >
                    {theme === Theme.Dark ? <Sun /> : <Moon />}
                </div>
            </Component>
        </div>
    );
};

export default ThemeSwitcher;
