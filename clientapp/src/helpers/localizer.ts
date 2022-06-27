import { useI18n, LocaleMessageDictionary, VueMessageType, LocaleMessageValue } from 'vue-i18n';

const localizeKey = 'localizeKey'

function useLocalizer() {
  let currentLocale = localStorage.getItem(localizeKey)

  const { t, locale, availableLocales } = useI18n({ useScope: 'global' })

  locale.value = currentLocale?.toString() == null ? "" : currentLocale

  return { t, locale, availableLocales }
}

function getAvailableLocales(){
  const { availableLocales } = useI18n({ useScope: 'global' })
  return availableLocales
}

function getLocale() {
  return localStorage.getItem(localizeKey) ?? ""
}

function setLocale(locale: string) {
  return localStorage.setItem(localizeKey, locale)
}

function getLocaleMessagesForKey(messageKey: string){
  const { getLocaleMessage } = useI18n({ useScope: 'global' })
  return getLocaleMessage(getLocale())[messageKey]
}

export default useLocalizer
export { getLocale, setLocale, useLocalizer, getAvailableLocales, getLocaleMessagesForKey }