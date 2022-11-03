import {host} from './configuration.json';

export const CFetch = (url, token, data) => {
  const options = {
    method: 'POST',
    headers: {
      'content-type': 'application/json',
      Authorization: 'Bearer ' + token,
    },
    body: JSON.stringify(data),
  };
  return fetch(host + '/Api/' + url, options);
};

export const CFormFetch = (url, token, formData) => {
  const options = {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'content-type': 'multipart/form-data',
      Authorization: 'Bearer ' + token,
    },
    body: formData,
  };
  return fetch(host + '/Api/' + url, options);
};
export default null;
