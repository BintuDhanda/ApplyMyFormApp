import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {ActivityIndicator} from 'react-native-paper';

const Message = ({
  height,
  message,
  allLoaded,
  color,
  indicatorSize,
  indicatorColor,
}) => {
  return (
    <View style={[styles.container, {height: height}]}>
      {allLoaded ? (
        <Text style={{color: color ? color : 'red'}}>{message}</Text>
      ) : (
        <ActivityIndicator size={indicatorSize} color={indicatorColor} />
      )}
    </View>
  );
};

export default Message;

const styles = StyleSheet.create({
  container: {
    width: '100%',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
