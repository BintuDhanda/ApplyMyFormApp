import React from 'react';
import {Dimensions, Modal, StyleSheet, View} from 'react-native';
import FastImage from 'react-native-fast-image';
import {ActivityIndicator} from 'react-native-paper';
import Colors from '../settings/Colors';
const Loader = ({loading, waiting, logo, spinner}) => {
  return (
    <Modal visible={loading} transparent={waiting}>
      <View
        style={[styles.container, !waiting && {backgroundColor: Colors.white}]}>
        {logo && (
          <FastImage
            source={require('../assets/images/logo.png')}
            style={{width: 200, height: 200}}
            resizeMode="center"
          />
        )}
        {spinner && (
          <ActivityIndicator
            count={6}
            color={Colors.primary}
            size={25}
            style={logo && {position: 'absolute', bottom: 80}}
          />
        )}
      </View>
    </Modal>
  );
};

const {height, width} = Dimensions.get('window');
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.transparentBlack,
    height: height,
    width: width,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
export default Loader;
