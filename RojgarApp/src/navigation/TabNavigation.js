import React from 'react';
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import Jobs from '../components/jobcomponents/Jobs';
import AdmitCard from '../components/jobcomponents/AdmitCard';
import Applied from '../components/jobcomponents/Applied';
import FontAwesome5 from 'react-native-vector-icons/dist/FontAwesome5';

const HomeName = 'HOME';
const ApplyName = 'APPLY';
const Admitname = 'ADMIT CARD';

const Tab = createBottomTabNavigator();

function TabNavigation() {
  return (
    <Tab.Navigator
      initialRouteName={HomeName}
      screenOptions={({route}) => ({
          headerShown: false,
        tabBarIcon: ({focused, color, size}) => {
          let iconname;
          let rn = route.name;
          if (rn == HomeName) {
            iconname = focused ? 'home' : 'home';
          } else if (rn == ApplyName) {
            iconname = focused ? 'envelope-open' : 'envelope';
          } else if (rn == Admitname) {
            iconname = focused ? 'address-card' : 'id-card';
          }
          return <FontAwesome5 name={iconname} size={20} color={color} />;
        }
      })}
      tabBarOptions={{
        activeTintColor: '#4CA6A8',
        inactiveTintColor: 'gray',
        labelStyle: {paddingBottom: 10, fontSize: 10},
        style: {padding: 10, height: 70},
      }}>
      <Tab.Screen name="HOME" component={Jobs} />
      <Tab.Screen name="APPLY" component={Applied} />
      <Tab.Screen name="ADMIT CARD" component={AdmitCard} />
    </Tab.Navigator>
  );
}
export default TabNavigation;
