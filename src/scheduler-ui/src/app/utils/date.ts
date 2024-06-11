import { format, parse } from 'date-fns';
import { enUS, uk } from 'date-fns/locale';

export const toUADay = (date: Date) =>
    format(date, 'EEEE, dd MMMM yyyy', { locale: uk });

export const toENDay = (date: Date) =>
    format(date, 'EEEE, dd MMMM yyyy', { locale: enUS });

export const toTimeOnlyNormalStyle = (time: string) => {
    const parsedTime = parse(time, 'HH:mm:ss.SSSSSSS', new Date());
    return format(parsedTime, 'HH:mm');
};
