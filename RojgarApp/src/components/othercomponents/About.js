import React from 'react';
import {View, Text, StyleSheet, TouchableOpacity, Linking} from 'react-native';
import Colors from '../../settings/Colors';

const About = () => {
  return (
    <>
      <View style={styles.container}>
        <Text style={styles.texts}>
          ApplyMyForm is founded by Mr. Amit Kumar. This App is created to make
          Form Apply Process easy for everyone. On this platform anybody can
          check latest Job Notifications,can Apply and Get Admit Card.{' '}
        </Text>
        <Text style={styles.heading}>
          We make it easy for you. Follow these steps :-
        </Text>
        <Text style={styles.texts}>1. Select Job You want to Apply</Text>
        <Text style={styles.texts}>2. Click on Apply</Text>
        <Text style={styles.texts}>
          3. Select The Post for You want to apply. You can select one or more
          post.
        </Text>
        <Text style={styles.texts}>
          4. Pay The fee that includes Application Fee + Form Filling Fee +
          Admit Card (If Applicable).
        </Text>
        <Text style={styles.texts}>
          5. Done. Now our team will apply your form on behalf of you.
        </Text>
        <Text style={styles.heading}>Founder</Text>
        <Text style={styles.texts}>
          Hi, I am Amit Kumar. I am from VPO Jhojhu Kalan, Charkhi Dadri. This
          app is created to make form apply online process easy. Thank You...
        </Text>
      </View>
      <View style={styles.devlopedby}>
        <TouchableOpacity onPress={() => Linking.openURL("https://quickapptechnologies.com/")}>
          <Text style={styles.linktext}>
            Developed By Quick App Technologies
          </Text>
        </TouchableOpacity>
      </View>
    </>
  );
};

export default About;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 15,
    paddingTop: 8,
  },
  heading: {
    fontSize: 15,
    fontWeight: '600',
    color: 'black',
    paddingBottom: 5,
  },
  texts: {
    fontSize: 13,
    fontWeight: '400',
    textAlign: 'justify',
    paddingBottom: 5,
    letterSpacing: 0.5,
    lineHeight: 20,
  },
  devlopedby: {
    alignItems: 'center',
    backgroundColor: Colors.white,
    padding: 5,
  },
  linktext: {
    fontSize: 15,
    fontWeight: '500',
    letterSpacing: 0.5,
    color: Colors.primary,
  },
});
