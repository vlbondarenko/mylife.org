import { useI18n, LocaleMessageDictionary, VueMessageType, LocaleMessageValue, VueI18n } from 'vue-i18n';
import axios from 'axios'
import { useCookies } from "vue3-cookies";

const API_URL = 'https://localhost:5001/api/';

const localizeKey = 'locale'

const { cookies } = useCookies()

function useLocalizer() {
  let currentLocale = getCurrentLocale()

  const { t, locale, availableLocales } = useI18n({ useScope: 'global' })

  locale.value = currentLocale?.toString() == null ? "" : currentLocale

  return { t, locale, availableLocales }
}

function getAvailableLocales() {
  const { availableLocales } = useI18n({ useScope: 'global' })
  return availableLocales
}

function getCurrentLocale() {
  return cookies.get(localizeKey) ?? ""
}

function setLocale(localeKey: string) {
  return axios.put(API_URL + "Localization", JSON.stringify(localeKey), 
  {
    headers: {
      'Content-Type': 'application/json'
    },
    withCredentials: true
  });
}

function getLocaleMessagesForKey(messageKey: string) {
  const { getLocaleMessage } = useI18n({ useScope: 'global' })
  return getLocaleMessage(getCurrentLocale())[messageKey]
}

export default useLocalizer
export { getCurrentLocale, setLocale, useLocalizer, getAvailableLocales, getLocaleMessagesForKey }