import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import FastImage from 'react-native-fast-image';
import {useNavigation} from '@react-navigation/native';
import {Button} from 'react-native-paper';

const WelcomePage = () => {
  const navigation = useNavigation();

  return (
    <View style={styles.container}>
      <FastImage
        source={require('../../assets/images/welcomeapp.png')}
        style={{width: '100%', height: 500}}
        resizeMode={FastImage.resizeMode.contain}
      />
      <View style={styles.titlecontainer}>
        <Text style={styles.Findtitle}>Find All Services</Text>
        <Text style={styles.Findtitle}>At one place</Text>
      </View>
      <View style={styles.desccontainer}>
        <Text style={styles.description}>
          All kind of services like Form Apply, Govt Scheme Apply etc.
        </Text>
        <Text style={styles.description}>Available at Home</Text>
      </View>
      <Button
        style={styles.startbutton}
        labelStyle={styles.btnlable}
        icon="arrow-right"
        onPress={() => navigation.navigate('Login')}>
        Lets's Get Started
      </Button>
    </View>
  );
};

export default WelcomePage;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    alignItems: 'center',
    justifyContent: 'center',
  },
  titlecontainer: {
    width: '100%',
    alignItems: 'center',
    justifyContent: 'center',
    textAlign: 'center',
  },
  Findtitle: {
    fontSize: 32,
    fontWeight: 'bold',
    color: 'black',
    letterSpacing: 1.2,
  },
  desccontainer: {
    width: '80%',
    alignItems: 'center',
    justifyContent: 'center',
    textAlign: 'center',
    paddingTop: 10,
  },
  description: {
    fontSize: 15,
    letterSpacing: 1,
    textAlign: 'center',
    lineHeight: 24
  },

  startbutton: {
    backgroundColor: '#4CA6A8',
    width: '60%',
    marginTop: 20,
    padding: 5,
  },
  btnlable: {
    color: 'white',
  },
});
