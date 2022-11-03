/**
 * @format
 */
import 'react-native-gesture-handler';
import {AppRegistry, Linking} from 'react-native';
import App from './App';
import {name as appName} from './app.json';
import PushNotification from 'react-native-push-notification';
import AsyncStorage from '@react-native-async-storage/async-storage';

PushNotification.configure({
  onRegister:async function ({token}) {
   await AsyncStorage.setItem('DeviceToken', token);
  },

  // (required) Called when a remote is received or opened, or local notification is opened
  onNotification: function (notification) {
    // process the notification
    // if(notification.userInteraction){
    //   var data = notification.data
    //   if(data){
    //     Linking.openURL("file:"+data.url)
    //   }
    // }

    // (required) Called when a remote is received or opened, or local notification is opened
    notification.finish();
  },
  popInitialNotification: true,
  requestPermissions: true,
});
AppRegistry.registerComponent(appName, () => App);
