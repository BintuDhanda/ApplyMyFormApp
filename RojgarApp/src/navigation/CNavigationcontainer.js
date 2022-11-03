import React from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {StatusBar} from 'react-native';

import Authnavigation from './Authnavigation';
import Stacknavigation from './Stacknavigation';
import Colors from '../settings/Colors';
const CNavigationcontainer = ({isLoggedIn}) => {
  return (
    <NavigationContainer>
      <StatusBar backgroundColor={Colors.primary} />
      {isLoggedIn ? <Stacknavigation /> : <Authnavigation />}
    </NavigationContainer>
  );
};
export default CNavigationcontainer;
