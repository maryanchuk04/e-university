import { useTranslations } from 'next-intl';
import React from 'react';

const Profile = () => {
	const t = useTranslations('profile');
    return <div>{t('label')}</div>;
};

export default Profile;
