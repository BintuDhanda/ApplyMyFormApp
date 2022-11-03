import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {ScrollView} from 'react-native-gesture-handler';
import Colors from '../../settings/Colors';

const CancelandRefunds = () => {
  return (
    <ScrollView>
      <View style={styles.container}>
        <Text style={styles.heading}>Returns</Text>
        <Text style={styles.texts}>
          Our policy lasts 3-4 days. If 3-4 days will not include holidays have gone by since your purchase,
          unfortunately we can’t offer you a refund or exchange.
        </Text>
        <Text style={styles.texts}>
          To be eligible for a return, your item must be unused and in the same
          condition that you received it. It must also be in the original
          packaging.
        </Text>
        <Text style={styles.texts}>
          Several types of goods are exempt from being returned. Perishable
          goods such as food, flowers, newspapers or magazines cannot be
          returned. We also do not accept products that are intimate or sanitary
          goods, hazardous materials, or flammable liquids or gases.
        </Text>
        <Text style={styles.heading}>Additional non-returnable items:</Text>
        <Text style={styles.texts}>Gift cards</Text>
        <Text style={styles.texts}>Downloadable software products</Text>
        <Text style={styles.texts}>Some Health and personal care items</Text>
        <Text style={styles.texts}>
          To complete your return, we require a receipt or proof of purchase.
          Please do not send your purchase back to the manufacturer. There are
          certain situations where only partial refunds are granted: (if
          applicable) Book with obvious signs of use CD, DVD, VHS tape,
          software, video game, cassette tape, or vinyl record that has been
          opened. Any item not in its original condition, is damaged or missing
          parts for reasons not due to our error. Any item that is returned more
          than 30 days after delivery
        </Text>
        <Text style={styles.heading}>Refunds (if applicable)</Text>
        <Text style={styles.texts}>
          Once your return is received and inspected, we will send you an email
          to notify you that we have received your returned item. We will also
          notify you of the approval or rejection of your refund. If you are
          approved, then your refund will be processed, and a credit will
          automatically be applied to your credit card or original method of
          payment, within a certain amount of days.
        </Text>
        <Text style={styles.heading}>
          Late or missing refunds (if applicable)
        </Text>
        <Text style={styles.texts}>
          If you haven’t received a refund yet, first check your bank account
          again. Then contact your credit card company, it may take some time
          before your refund is officially posted.
        </Text>
        <Text style={styles.texts}>
          Next contact your bank. There is often some processing time before a
          refund is posted. If you’ve done all of this and you still have not
          received your refund yet, please contact us at info@applymyform.in
        </Text>
        <Text style={styles.heading}>Sale items (if applicable)</Text>
        <Text style={styles.texts}>
          Only regular priced items may be refunded, unfortunately sale items
          cannot be refunded.
        </Text>
        <Text style={styles.heading}>Exchanges (if applicable)</Text>
        <Text style={styles.texts}>
          We only replace items if they are defective or damaged. If you need to
          exchange it for the same item, send us an email at info@applymyform.in
          and send your item to: Apply my form App, VPO Jhojhu Kalan, Charkhi
          Dadri.
        </Text>
        <Text style={styles.heading}>Gifts</Text>
        <Text style={styles.texts}>
          If the item was marked as a gift when purchased and shipped directly
          to you, you’ll receive a gift credit for the value of your return.
          Once the returned item is received, a gift certificate will be mailed
          to you.
        </Text>
        <Text style={styles.texts}>
          If the item wasn’t marked as a gift when purchased, or the gift giver
          had the order shipped to themselves to give to you later, we will send
          a refund to the gift giver and he will find out about your return.
        </Text>

        <Text style={styles.heading}>Shipping</Text>
        <Text style={styles.texts}>
          To return your product, you should mail your product to: Apply My
          Form, VPO Jhojhu Kalan, Charkhi Dadri, Haryana. You will be
          responsible for paying for your own shipping costs for returning your
          item. Shipping costs are non-refundable. If you receive a refund, the
          cost of return shipping will be deducted from your refund.
        </Text>
        <Text style={styles.texts}>
          Depending on where you live, the time it may take for your exchanged
          product to reach you, may vary.
        </Text>
        <Text style={styles.texts}>
          If you are shipping an item over $75, you should consider using a
          trackable shipping service or purchasing shipping insurance. We don’t
          guarantee that we will receive your returned item.
        </Text>
      </View>
    </ScrollView>
  );
};

export default CancelandRefunds;

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
});
