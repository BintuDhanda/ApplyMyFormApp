import React from 'react';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import Login from '../components/authcomponents/Login';
import Signup from '../components/authcomponents/Signup';
import Confirmaccount from '../components/authcomponents/Confirmaccount';
import Forgetpassword from '../components/authcomponents/Forgetpassword';
import Resetpassword from '../components/authcomponents/Resetpassword';
import WelcomePage from '../components/authcomponents/WelcomePage';

const Stack = createNativeStackNavigator();

const Authnavigation = () => {
  return (
    <Stack.Navigator screenOptions={{headerShown: false}}>
      <Stack.Screen name="Welcome" component={WelcomePage} />
      <Stack.Screen name="Login" component={Login} />
      <Stack.Screen name="Signup" component={Signup} />
      <Stack.Screen name="ConfirmAccount" component={Confirmaccount} />
      <Stack.Screen name="ForgetPassword" component={Forgetpassword} />
      <Stack.Screen name="ResetPassword" component={Resetpassword} />
    </Stack.Navigator>
  );
};

export default Authnavigation;
