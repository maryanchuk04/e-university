'use client';

import React, { useState } from 'react';
import { Dropdown, DropdownTrigger, DropdownMenu, DropdownItem, Button } from '@nextui-org/react';
import { Language } from '../../../core/models/Language';
import { useRouter, usePathname } from 'next/navigation';
import { useLocale } from 'next-intl';

interface LanguageDropdownItem {
    lng: Language;
    name: string;
    flag: string;
}

const languages: LanguageDropdownItem[] = [
    { lng: Language.ua, name: 'Українська', flag: '/images/ua.png' },
    { lng: Language.en, name: 'English', flag: '/images/en.png' },
];

const LanguageSwitcher = () => {
    const locale = useLocale();
    const router = useRouter();
    const pathname = usePathname();

    const [lng, setLng] = useState<LanguageDropdownItem>(languages.find((l) => l.lng === locale) ?? languages[0]);

    const changeLanguage = (selectedLng: LanguageDropdownItem) => {
        setLng(selectedLng);
        router.push(pathname.replace(locale, selectedLng.lng));
    };

    return (
        <Dropdown>
            <DropdownTrigger>
                <Button variant='flat' className='capitalize h-11 bg-default-100 w-fit hover:bg-default-200 dark:bg-primary'>
                    <div className='flex align-items-center gap-1'>
                        <img src={lng.flag} className='h-full rounded-sm w-8' alt={lng.name} />
                    </div>
                </Button>
            </DropdownTrigger>
            <DropdownMenu aria-label='language select' variant='flat' disallowEmptySelection>
                {languages.map((l: LanguageDropdownItem) => (
                    <DropdownItem isSelected={l.lng === lng.lng} key={l.lng} onClick={() => changeLanguage(l)} textValue={l.name}>
                        <div className='flex gap-1'>
                            <img src={l.flag} className='h-full rounded-sm w-8' alt={l.name} />
                            {l.name}
                        </div>
                    </DropdownItem>
                ))}
            </DropdownMenu>
        </Dropdown>
    );
};

export default LanguageSwitcher;
