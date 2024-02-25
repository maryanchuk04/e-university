import React from 'react';
import { useTranslations } from 'next-intl';
import { Button, Divider, Link } from '@nextui-org/react';
import { AuthenticateUISettings } from '@/app/models/authenticate-ui-settings';
import ThemeSwitcher from '@/app/components/ThemeSwitcher';
import LanguageSwitcher from '@/app/components/LanguageSwitcher';

const Authenticate = () => {
    const authenticateSettings: AuthenticateUISettings = {
        domain: 'chnu.edu.ua',
        logo: '/images/logo-chnu.svg',
        backgroundImage: '/images/chnu-front.jpg',
        links: {
            tiktok: '',
            youtube: '',
            website: '',
        },
    };

    const t = useTranslations('authenticate');

    return (
        <div className='flex w-full h-screen relative'>
            <div className='absolute top-3 left-3 flex gap-2'>
                <ThemeSwitcher />
                <LanguageSwitcher />
            </div>
            <div className='w-1/2 flex flex-col h-full justify-between py-8'>
                <form className='h-full grid place-items-center'>
                    <div className='w-1/2 h-full text-center flex flex-col justify-center align-items-center'>
                        <h2 className='text-4xl text-center font-bold mb-8'>{t('welcome')}</h2>
                        <Button color='primary' className='text-2xl p-8 w-full flex align-items-center' size='lg' variant='shadow'>
                            <img src='/images/google.png' className='w-12' />
                            <span className='font-bold'>{authenticateSettings.domain}</span>
                        </Button>
                        <Divider className='my-6' />
                        <p className=''>{t('org_email_requirements')}</p>
                    </div>
                </form>
                <Divider className='my-6 mx-auto w-1/2' />
                <div className='h-1/12 mx-auto w-1/3 flex justify-between text-md'>
                    <Link href={t.raw('link.website')} isBlock color='foreground'>
                        Веб-сайт
                    </Link>
                    <Link isBlock color='foreground'>
                        {t('link.youtube')}
                    </Link>
                    <Link isBlock color='foreground'>
                        {t('link.tiktok')}
                    </Link>
                </div>
            </div>

            <div className='w-1/2 h-full relative'>
                <img
                    className='w-full h-full object-cover object-center'
                    src={authenticateSettings.backgroundImage}
                    alt={`${authenticateSettings.domain}-background`}
                />
                <div className='absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 backdrop-blur-md p-6 rounded-xl bg-white/40'>
                    <img className='w-[500px]' src={authenticateSettings.logo} alt={`${authenticateSettings.domain}-logo`} />
                </div>
            </div>
        </div>
    );
};

export default Authenticate;
