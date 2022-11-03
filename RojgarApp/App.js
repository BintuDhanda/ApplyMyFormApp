import React, {useEffect, useState} from 'react';
import {AppContext, AuthContext} from './src/settings/Context';
import CNavigationcontainer from './src/navigation/CNavigationcontainer';
import AsyncStorage from '@react-native-async-storage/async-storage';
import Loader from './src/utilities/Loader';
import {LogBox} from 'react-native';

LogBox.ignoreAllLogs();
const App = () => {
  const [loading, setLoading] = useState(true);

  // Auth Actions using Reducer start //
  const initialLoginState = {
    loading: true,
    userName: null,
    userToken: null,
  };
  const loginReducer = (prevState, action) => {
    switch (action.type) {
      case 'RETRIEVE_TOKEN':
        return {
          ...prevState,
          userToken: action.token,
          userName: action.userName,
          loading: false,
        };
      case 'LOGIN':
        return {
          ...prevState,
          userName: action.id,
          userToken: action.token,
          loading: false,
        };
      case 'LOGOUT':
        return {
          ...prevState,
          userName: null,
          userToken: null,
          loading: false,
        };
      case 'REGISTER':
        return {
          ...prevState,
          userName: action.id,
          userToken: action.token,
          loading: false,
        };
    }
  };
  const [loginState, dispatch] = React.useReducer(
    loginReducer,
    initialLoginState,
  );
  const [appUser, setAppUser] = useState({
    token: '',
    userName: '',
    fullName: '',
    phoneNumber: '',
    userRole: '',
  });
  const signOut = async () => {
    await AsyncStorage.removeItem('userToken');
    await AsyncStorage.removeItem('userName');
    await AsyncStorage.removeItem('userRole');
    await AsyncStorage.removeItem('fullName');
    await AsyncStorage.removeItem('phoneNumber');
    setAppUser({
      token: '',
      userName: '',
      fullName: '',
      phoneNumber: '',
      userRole: '',
    });
  };
  let userToken = null;
  let userName = null;
  let userRole = null;
  let fullName = null;
  let phoneNumber = null;
  const GetUser = async () => {
    try {
      userToken = await AsyncStorage.getItem('userToken');
      userName = await AsyncStorage.getItem('userName');
      userRole = await AsyncStorage.getItem('userRole');
      fullName = await AsyncStorage.getItem('fullName');
      phoneNumber = await AsyncStorage.getItem('phoneNumber');
      setAppUser({
        ...appUser,
        userName: userName,
        userRole: userRole,
        token: userToken,
        fullName: fullName,
        phoneNumber:
          phoneNumber != '' && phoneNumber != null
            ? phoneNumber.toString()
            : phoneNumber,
      });
    } catch (e) {
      console.log(e);
    }
  };
  useEffect(() => {
    GetUser().finally(() => {
      setLoading(false);
    });
  }, []);

  if (loading) {
    return <Loader loading={loading} spinner={true} logo={true} />;
  }

  return (
    <AuthContext.Provider value={{signOut}}>
      <AppContext.Provider value={{appUser, setAppUser}}>
        <CNavigationcontainer isLoggedIn={appUser.token ? true : false} />
      </AppContext.Provider>
    </AuthContext.Provider>
  );
};

export default App;
