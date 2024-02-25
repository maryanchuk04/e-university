type Messages = typeof import('./translations/en.json');
type UAMessage = typeof import('./translations/ua.json');

declare interface IntlMessages extends Messages, UAMessage {}