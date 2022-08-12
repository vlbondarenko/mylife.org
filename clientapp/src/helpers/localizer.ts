import { useI18n, LocaleMessageDictionary, VueMessageType, LocaleMessageValue } from 'vue-i18n';
import axios from 'axios'
import { useCookies } from "vue3-cookies";

const API_URL = 'https://localhost:5001/api/';

const localizeKey = '.AspNetCore.Culture'

const { cookies } = useCookies()

function useLocalizer() {
  let currentLocale = getLocale()

  const { t, locale, availableLocales } = useI18n({ useScope: 'global' })

  locale.value = currentLocale?.toString() == null ? "" : currentLocale

  return { t, locale, availableLocales }
}

function getAvailableLocales() {
  const { availableLocales } = useI18n({ useScope: 'global' })
  return availableLocales
}

function getLocale() {
  let regexp = new RegExp('(?<=c=)[a-zA-Z,-]+')
  let locale = cookies.get(localizeKey) ?? ""
  let result = regexp.exec(locale) || []

  return result[0]
}

function setLocale(locale: string) {
  return axios.put(API_URL + "Localization/locale", JSON.stringify(locale), 
  {
    headers: {
      'Content-Type': 'application/json'
    },
    withCredentials: true
  });
}

function getLocaleMessagesForKey(messageKey: string) {
  const { getLocaleMessage } = useI18n({ useScope: 'global' })
  return getLocaleMessage(getLocale())[messageKey]
}

export default useLocalizer
export { getLocale, setLocale, useLocalizer, getAvailableLocales, getLocaleMessagesForKey }