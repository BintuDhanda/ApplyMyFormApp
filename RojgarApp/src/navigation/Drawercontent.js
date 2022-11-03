import React, {useContext} from 'react';
import {ImageBackground, Image, Text, View, StyleSheet} from 'react-native';
import {DrawerContentScrollView, DrawerItem} from '@react-navigation/drawer';
import FontAwesome5 from 'react-native-vector-icons/dist/FontAwesome5';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';
import {AppContext, AuthContext} from '../settings/Context';
import Colors from '../settings/Colors';

const Drawercontent = props => {
  const {signOut} = useContext(AuthContext);
  const {appUser} = useContext(AppContext);
  const CustomNavigator = screen => {
    props.navigation.navigate(screen);
    props.navigation.closeDrawer();
  };
  return (
    <View style={{flex: 1}}>
      <DrawerContentScrollView {...props} showsVerticalScrollIndicator={false}>
        <ImageBackground
          source={require('../assets/images/bgdrawer.png')}
          style={styles.profileContainer}>
          <Image
            source={require('../assets/images/user.png')}
            style={{width: 80, height: 80, borderRadius: 80}}
          />
          <View>
            <Text
              numberOfLines={2}
              style={{color: '#fff', width: 150, fontSize: 18, paddingLeft: 5}}>
              Welcome ! {appUser.fullName}
            </Text>
            <Text style={{color: '#fff', fontSize: 18, paddingLeft: 5}}>
              {appUser.phoneNumber}
            </Text>
          </View>
        </ImageBackground>
        <DrawerItem
          onPress={() => CustomNavigator('HomeScreen')}
          label="Home"
          icon={() => (
            <FontAwesome5 name="home" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('Qualifications')}
          label="Qualifications"
          icon={() => (
            <FontAwesome5
              name="user-graduate"
              size={20}
              color={Colors.primary}
            />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('Documents')}
          label="Documents"
          icon={() => (
            <FontAwesome5
              name="user-graduate"
              size={20}
              color={Colors.primary}
            />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('Experiences')}
          label="Experiences"
          icon={() => (
            <FontAwesome5 name="briefcase" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('PersonalDetails')}
          label="Personal Details"
          icon={() => (
            <FontAwesome5 name="user-edit" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('ApplicationHistory')}
          label="Application History"
          icon={() => (
            <FontAwesome5 name="history" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('Privacypolicy')}
          label="Privacy Policy"
          icon={() => (
            <FontAwesome5
              name="product-hunt"
              size={20}
              color={Colors.primary}
            />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('CancelandRefund')}
          label="Cancel & Refund"
          icon={() => (
            <FontAwesome5 name="text-height" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('Termsandconditions')}
          label="Terms & Conditions"
          icon={() => (
            <FontAwesome5 name="text-height" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('About')}
          label="About Us"
          icon={() => (
            <FontAwesome5
              name="address-card"
              size={20}
              color={Colors.primary}
            />
          )}
        />
        <DrawerItem
          onPress={() => CustomNavigator('ChangePassword')}
          label="Change Password"
          icon={() => (
            <FontAwesome name="key" size={20} color={Colors.primary} />
          )}
        />
        <DrawerItem
          onPress={() => signOut()}
          label="Sign Out"
          icon={() => (
            <FontAwesome name="sign-out" size={20} color={Colors.primary} />
          )}
        />
      </DrawerContentScrollView>
    </View>
  );
};

export default Drawercontent;

const styles = StyleSheet.create({
  profileContainer: {
    padding: 20,
    top: -4,
    flexDirection: 'row',
    alignItems: 'center',
  },
});
