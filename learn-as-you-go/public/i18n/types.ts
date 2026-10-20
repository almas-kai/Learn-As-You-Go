import en from './en.json';

type Paths<T, Prefix extends string = ''> = T extends string
  ? (Prefix extends '' ? never : Prefix)
  : { [K in keyof T & string]: Paths<T[K], `${Prefix}${Prefix extends '' ? '' : '.'}${K}`> }[keyof T & string];

export type TranslationKey = Paths<typeof en>;