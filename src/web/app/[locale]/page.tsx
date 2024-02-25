import { useTranslations } from 'next-intl';

const Home = () => {
    const t = useTranslations();
    return (
        <div>
            <h1>{t('test')}</h1>
        </div>
    );
};

export default Home;
