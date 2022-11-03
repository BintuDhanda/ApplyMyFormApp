import React from 'react';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import Drawernavigation from './Drawernavigation';
import JobDetail from '../components/jobcomponents/JobDetail';
import Changepassword from '../components/authcomponents/Changepassword';
import ApplicationDetail from '../components/applicationhistorycomponents/ApplicationDetail';
const Stack = createNativeStackNavigator();
const Stacknavigation = () => {
  return (
    <Stack.Navigator>
      <Stack.Screen
        options={{headerShown: false}}
        name="DrawerHome"
        component={Drawernavigation}
      />
      <Stack.Screen
        name="JobDetail"
        options={{headerTitle: 'Job Detail'}}
        component={JobDetail}
      />
      <Stack.Screen
        name="ApplicationDetail"
        options={{headerTitle: 'Application Detail'}}
        component={ApplicationDetail}
      />
      <Stack.Screen
        name="ChangePassword"
        options={{headerTitle: 'Change Password'}}
        component={Changepassword}
      />
    </Stack.Navigator>
  );
};
export default Stacknavigation;
